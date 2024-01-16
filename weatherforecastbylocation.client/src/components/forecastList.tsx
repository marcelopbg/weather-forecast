import { Forecast } from "../types/forecast";
import { Period } from "../types/period";

const getTodaysTemperature = (periods: Period[]): string => {
    let result: string = ''


    switch (periods.length) {
        case 1:
            result = periods[0].temperature.toString()
            break;
        case 2:
            result = `${periods[0].temperature}º ${periods[0].temperatureUnit} / ${periods[1].temperature}º ${periods[1].temperatureUnit}`
            break;
        default:
            break;
    }
    return result;
}

interface ForecastListProps {
    forecasts: Forecast[]
    selectedForecast: Forecast | undefined
    handleForecastClick: (forecast: Forecast) => void;
}

const ForecastList: React.FC<ForecastListProps> = ({ forecasts, selectedForecast, handleForecastClick }) =>
(forecasts && forecasts.length > 1 && 
    <div className="forecast-container weekly-forecast">
        <h2>Next 7 days forecast</h2>
        <ul>
            {forecasts.map((forecast, index) => (
                <li
                    key={index}
                    onClick={() => handleForecastClick(forecast)}
                    className={forecast === selectedForecast ? 'selected' : ''}
                >
                    {forecast!.periods![0].name}: {getTodaysTemperature(forecast.periods!)}
                </li>
            ))}
        </ul>
    </div>
)

export default ForecastList;