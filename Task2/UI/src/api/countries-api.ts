import { ListResponse } from "../models/ListItem";
import { WeatherResponse } from "../models/Weather";
import axiosInstance from "../utils/axios";

const COUNTRIES_PATH = "/countries";

export const axiosFetchCountries = async (): Promise<ListResponse> => {
  const response = await axiosInstance.get(COUNTRIES_PATH);

  return response.data;
};

export const axiosFetchCountryCities = async (
  countryId: string
): Promise<ListResponse> => {
  const response = await axiosInstance.get(
    `${COUNTRIES_PATH}/${countryId}/cities`
  );

  return response.data;
};

export const axiosFetchCityWeathers = async (
  countryId: string,
  cityId: string
): Promise<WeatherResponse> => {
  const endDate = new Date();
  const startDate = new Date();
  startDate.setDate(startDate.getDate() - 1);

  const response = await axiosInstance.get(
    `${COUNTRIES_PATH}/${countryId}/cities/${cityId}?start=${startDate.toISOString()}&end=${endDate.toISOString()}`
  );

  return response.data;
};
