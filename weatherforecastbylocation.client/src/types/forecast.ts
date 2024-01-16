import { Period } from "./period"

export interface Forecast {
    date?: string
    periods?: Period[]
}