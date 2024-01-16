import './App.css';

import { useState } from 'react';
import { Forecast } from './types/forecast';
import SearchInput from './components/searchInput';
import LoadingOverlay from './components/loading';
import SelectedForecast from './components/selectedForecast';
import ForecastList from './components/forecastList';
import toast, { Toaster } from 'react-hot-toast';

const App = () => {

    const [search, setSearch] = useState<string>("");
    const [forecasts, setForecasts] = useState<Forecast[]>([])
    const [selectedForecast, setSelectedForecast] = useState<Forecast>();
    const [isLoading, setIsLoading] = useState(false);

    const populateWeatherData = async () => {
        try {
            const response = await fetch(`weatherforecast/${search}`);
            if (response.status === 404 || response.status === 400) throw new Error("Postal Address not found. Check your input and try again")
            if (response.status != 200) throw new Error("An error occured. Please try again later.")
            
            const data = await response.json();
            setForecasts(data);
            setSelectedForecast(data[0]);
        }
        catch (e: unknown) {
            const err = e as Error;
            toast.error(err.message, { position: 'top-right' })
        }
    }

    const handleSearch = async () => {
        setIsLoading(true);
        await populateWeatherData();
        setIsLoading(false)
    }

    const handleForecastClick = (forecast: Forecast): void => {
        setSelectedForecast(forecast)
    }

    return (
        <>
            <div className='header'>
                <h2>Weather App</h2> <img width={40} src='../public/sun.svg'></img>
            </div>
            <LoadingOverlay isLoading={isLoading} />
            <Toaster />
            <div className="container">
                <SearchInput
                    disabled={isLoading || !search || search === ""}
                    search={search}
                    setSearch={setSearch}
                    handleSearch={handleSearch}
                    />
                <div className='content'>
                    <SelectedForecast
                        selectedForecast={selectedForecast}
                    />
                    <ForecastList
                        forecasts={forecasts}
                        selectedForecast={selectedForecast}
                        handleForecastClick={handleForecastClick}
                    />
                    <div />
                </div>
            </div>
        </>
    );
};

export default App;
