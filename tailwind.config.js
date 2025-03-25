/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './src/**/*.{js,ts,jsx,tsx}', // Include all components
    './src/components/ui/**/*.{js,ts,jsx,tsx}' // Include shadcn components
  ],
  theme: {
    extend: {},
  },
  plugins: [],
}

