import { MonkeySpecies } from "../enums/species";

export interface MonkeyReportResponse{
    Id:number,
    Name:string,
    Weight:number,
    Species:MonkeySpecies
    LastUpdateTime:Date
}