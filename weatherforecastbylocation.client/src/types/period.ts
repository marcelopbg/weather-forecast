import { ValueWrapper } from "./valueWrapper"

export interface Period {
    startTime: Date
    endTime: Date
    icon: string
    isDayTime: boolean
    longForecast?: string
    name: string
    probabilityOfPrecipitation: ValueWrapper
    relativeHumidity: ValueWrapper
    shortForecast: string
    temperature: number
    windDirection: string
    windSpeed: string
    temperatureUnit: string
}