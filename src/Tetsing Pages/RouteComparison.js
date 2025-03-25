import { decode } from '@mapbox/polyline';
import { lineString, polygon, booleanPointInPolygon } from '@turf/turf';

export default function RouteComparison(encodedPolyline1, encodedPolyline2) {
  try {
    const route1Coords = decode(encodedPolyline1);
    const route2Coords = decode(encodedPolyline2);

    // Ensure the polygon is closed by duplicating the first coordinate at the end
    const closedRoute1Coords = [...route1Coords, route1Coords[0]];
    const closedRoute2Coords = [...route2Coords, route2Coords[0]];

    const route1Line = lineString(route1Coords);
    const route2Line = lineString(route2Coords);

    const route1Polygon = polygon([closedRoute1Coords]);
    const route2Polygon = polygon([closedRoute2Coords]);

    for (const coord of route1Coords) { 
      const point = { type: 'Feature', geometry: { type: 'Point', coordinates: coord } };
      if (booleanPointInPolygon(point, route2Polygon)) {
        return "Yes";
      }
    }

    return "No";
  } catch (error) {
    console.error("Error checking route containment:", error);
    return "Error";
  }
}