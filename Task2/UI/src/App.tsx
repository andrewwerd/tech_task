import { useState } from "react";
import "./App.css";
import { Box, Typography } from "@mui/material";
import CountrySelect from "./components/CountrySelect";
import CitySelect from "./components/CitySelect";
import WeatherChart from "./components/WeatherChart";

function App() {
  const [countryId, setCountryId] = useState<string>("");
  const [cityId, setCityId] = useState<string>("");

  return (
    <>
      <Box sx={{ height: "100vh", width: "100vw", bgcolor: "#dfdfdfa6" }}>
        <Box sx={{ width: "100%", height: "15%", display: "flex" }}>
          <CountrySelect onCountrySelect={(country) => setCountryId(country)} />
          {countryId ? (
            <CitySelect
              countryId={countryId}
              onCitySelect={(city) => setCityId(city)}
            />
          ) : (
            <></>
          )}
        </Box>
        <Box
          sx={{
            width: "100%",
            height: "85%",
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
          }}
        >
          {!countryId ? (
            <Typography> Please, select country!</Typography>
          ) : (
            <>
              {!cityId ? (
                <Typography> Please, select city!</Typography>
              ) : (
                <>
                  <WeatherChart cityId={cityId} countryId={countryId} />
                </>
              )}
            </>
          )}
        </Box>
      </Box>
    </>
  );
}

export default App;
