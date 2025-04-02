import { Line } from "react-chartjs-2";
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend,
} from "chart.js";
import { WeatherItem } from "../models/Weather";
import { useState, useEffect } from "react";
import { axiosFetchCityWeathers } from "../api/countries-api";
import { Box, CircularProgress } from "@mui/material";

ChartJS.register(
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend
);

interface WeatherChartProps {
  countryId: string;
  cityId: string;
}

const WeatherChart = (props: WeatherChartProps) => {
  const [weatherItems, setWeatherItems] = useState<WeatherItem[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      setError(null);
      try {
        const response = await axiosFetchCityWeathers(
          props.countryId,
          props.cityId
        );
        setWeatherItems(response.data);
        // eslint-disable-next-line @typescript-eslint/no-unused-vars
      } catch (err) {
        setError("Failed to fetch countries");
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [props.countryId, props.cityId]);

  const timestamps = weatherItems.map((item) =>
    new Date(item.timestamp).toUTCString()
  );
  const minTemps = weatherItems.map((item) => item.minTemperature);
  const maxTemps = weatherItems.map((item) => item.maxTemperature);

  const data = {
    labels: timestamps,
    datasets: [
      {
        label: "Min Temperature",
        data: minTemps,
        fill: false,
        borderColor: "rgb(75, 192, 192)",
        tension: 0.1,
      },
      {
        label: "Max Temperature",
        data: maxTemps,
        fill: false,
        borderColor: "rgb(255, 99, 132)",
        tension: 0.1,
      },
    ],
  };

  const options = {
    responsive: true,
    plugins: {
      title: {
        display: true,
        text: "Temperature Chart",
      },
      tooltip: {
        mode: "index",
        intersect: false,
      },
    },
    scales: {
      x: {
        type: "category",
        title: {
          display: true,
          text: "Timestamp",
        },
      },
      y: {
        min: 0,
        title: {
          display: true,
          text: "Temperature (°C)",
        },
      },
    },
  };

  return (
    <Box
      sx={{
        width: "100%",
        height: "100%",
        display: "flex",
        flexDirection: "column",
        justifyContent: "center",
        alignItems: "center",
      }}
    >
      {loading ? <CircularProgress color="inherit" size={20} /> : <></>}
      {error ? <>Error fetching data</> : <></>}
      {error === null && !loading ? (
        <Line data={data} options={options} />
      ) : (
        <></>
      )}{" "}
    </Box>
  );
};

export default WeatherChart;
