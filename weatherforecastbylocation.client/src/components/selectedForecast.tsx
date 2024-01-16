import { Forecast } from "../types/forecast"

interface SelectedForecastProps { 
    selectedForecast: Forecast | undefined
}

const getTemperature = (temperature: number, unit: string): string => {
    return `${temperature}º ${unit}`
}

const SelectedForecast : React.FC<SelectedForecastProps> = ({ selectedForecast }) =>
    <div className='left-wrap'>
    {selectedForecast?.periods && selectedForecast.periods.map((period, index) => {
        return (
            <div key={index} className="forecast-container period-forecast">

                <div className='d-flex'><h3> {period.name} </h3> <img className="forecast-icon" src={period.icon}></img></div>
                <h3 style={{ marginTop: '5px' }}>{getTemperature(period.temperature, period.temperatureUnit)} - {period.longForecast ?? period.shortForecast}</h3>
                <table className="forecast-grid">
                    <thead>
                        <tr>
                            <th>Risk of Rain</th>
                            <th>Humidity</th>
                            <th>Wind Direction</th>
                            <th>Wind Speed</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>{period.probabilityOfPrecipitation.value ?? 0}%</td>
                            <td>{period.relativeHumidity.value ?? 0}%</td>
                            <td>{period.windDirection}</td>
                            <td>{period.windSpeed}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        )
    })}
</div>

export default SelectedForecast