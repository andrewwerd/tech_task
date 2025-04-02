export interface WeatherItem {
  timestamp: Date;
  minTemperature: number;
  maxTemperature: number;
}

export interface WeatherResponse {
  data: WeatherItem[];
}
