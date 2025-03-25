import React, { useState, useEffect, useRef } from "react"
import axios from "axios"
import { Search, Loader } from 'lucide-react'

const AutocompleteTextbox = ({ onSelection }) => {
  const [inputValue, setInputValue] = useState("")
  const [suggestions, setSuggestions] = useState([])
  const [loading, setLoading] = useState(false)
  const [location, setLocation] = useState(null)
  const [isFocused, setIsFocused] = useState(false)
  const inputRef = useRef(null)
  const suggestionsRef = useRef(null)

  const apiKey = "zfREBrvVLEZgcmMDr5SXwI9qYyvTSrJymkqiZICy"

  useEffect(() => {
    const fetchLocation = () => {
      if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
          (position) => {
            const { latitude, longitude } = position.coords
            setLocation(`${latitude},${longitude}`)
          },
          (error) => {
            console.error("Error getting location:", error)
            setLocation("12.931316595874005,77.61649243443775") // Default location (fallback)
          },
        )
      } else {
        console.log("Geolocation not supported")
        setLocation("12.931316595874005,77.61649243443775") // Default location (fallback)
      }
    }

    fetchLocation()

    const handleClickOutside = (event) => {
      if (
        inputRef.current &&
        !inputRef.current.contains(event.target) &&
        suggestionsRef.current &&
        !suggestionsRef.current.contains(event.target)
      ) {
        setIsFocused(false)
      }
    }

    document.addEventListener("mousedown", handleClickOutside)
    return () => {
      document.removeEventListener("mousedown", handleClickOutside)
    }
  }, [])

  const handleInputChange = async (e) => {
    const value = e.target.value
    setInputValue(value)

    if (value.trim() === "") {
      setSuggestions([])
      return
    }

    if (!location) return

    setLoading(true)

    try {
      const response = await axios.get(`https://api.olamaps.io/places/v1/autocomplete`, {
        params: {
          input: value,
          location: location,
          api_key: apiKey,
        },
      })

      setSuggestions(response.data.predictions || [])
    } catch (error) {
      console.error("Error fetching autocomplete suggestions:", error)
      setSuggestions([])
    } finally {
      setLoading(false)
    }
  }

  const handleSuggestionClick = async (suggestion) => {
    setInputValue(suggestion.structured_formatting.main_text)
    setSuggestions([])
    setIsFocused(false)

    try {
      const response = await axios.get(`https://api.olamaps.io/places/v1/details`, {
        params: {
          place_id: suggestion.place_id,
          api_key: apiKey,
        },
      })

      const locationData = response.data.result.geometry.location
      const placeDetails = {
        name: suggestion.structured_formatting.main_text,
        address: suggestion.structured_formatting.secondary_text,
        address_components: response.data.result.address_components,
        latitude: locationData.lat,
        longitude: locationData.lng,
      }

      if (onSelection) {
        onSelection(placeDetails)
      }
    } catch (error) {
      console.error("Error fetching place details:", error)
    }
  }

  return (
    <div className="relative w-full">
      <div className="relative">
        <input
          ref={inputRef}
          type="text"
          value={inputValue}
          onChange={handleInputChange}
          onFocus={() => setIsFocused(true)}
          placeholder="Start typing..."
          className="w-full px-5 py-3 pr-12 text-base text-gray-700 bg-white border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent"
        />
        <div className="absolute inset-y-0 right-0 flex items-center pr-4 pointer-events-none">
          {loading ? (
            <Loader className="w-6 h-6 text-gray-400 animate-spin" />
          ) : (
            <Search className="w-6 h-6 text-gray-400" />
          )}
        </div>
      </div>
      {isFocused && suggestions.length > 0 && (
        <ul
          ref={suggestionsRef}
          className="absolute z-10 w-full mt-1 bg-white border border-gray-300 rounded-md shadow-lg max-h-60 overflow-auto"
        >
          {suggestions.map((suggestion, index) => (
            <li
              key={index}
              onClick={() => handleSuggestionClick(suggestion)}
              className="px-5 py-3 hover:bg-gray-100 cursor-pointer transition duration-150 ease-in-out"
            >
              <div className="text-base font-medium text-gray-700">{suggestion.structured_formatting.main_text}</div>
              <div className="text-sm text-gray-500">{suggestion.structured_formatting.secondary_text}</div>
            </li>
          ))}
        </ul>
      )}
    </div>
  )
}

export default AutocompleteTextbox
