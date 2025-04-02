import { useState, useEffect } from "react";
import { Autocomplete, TextField, CircularProgress, Box } from "@mui/material";
import { ListItem } from "../models/ListItem";
import { axiosFetchCountries } from "../api/countries-api";

interface CountrySelectProps {
  onCountrySelect: (country: string) => void;
}

const CountrySelect = (props: CountrySelectProps) => {
  const [countries, setCountries] = useState<ListItem[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      setError(null);
      try {
        const response = await axiosFetchCountries();
        setCountries(response.data);
        // eslint-disable-next-line @typescript-eslint/no-unused-vars
      } catch (err) {
        setError("Failed to fetch countries");
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  return (
    <Box width={"200px"} margin={"8px"}>
      <Autocomplete
        disablePortal
        id="country-select"
        options={countries}
        getOptionLabel={(option) => option.name}
        renderInput={(params) => (
          <TextField
            {...params}
            label="Select Country"
            variant="outlined"
            InputProps={{
              ...params.InputProps,
              endAdornment: (
                <>
                  {loading ? (
                    <CircularProgress color="inherit" size={20} />
                  ) : null}
                  {params.InputProps.endAdornment}
                </>
              ),
            }}
          />
        )}
        isOptionEqualToValue={(option, value) => option.id === value.id}
        onChange={(_, value) => {
          props.onCountrySelect(value?.id ?? "");
        }}
      />
      {error && <div style={{ color: "red" }}>{error}</div>}
    </Box>
  );
};

export default CountrySelect;
