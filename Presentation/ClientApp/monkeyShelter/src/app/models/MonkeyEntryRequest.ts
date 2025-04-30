import { MonkeySpecies } from "../enums/species";

export interface MonkeyEntryRequest {
    name: string;
    species: MonkeySpecies;
    weight: number;
  }
  