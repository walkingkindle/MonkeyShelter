import { MonkeyCheckupResponse } from "./MonkeyCheckupResponse";

export interface MonkeyVetCheckResponse{
    scheduledInTheNext30:MonkeyCheckupResponse[],
    upcomingVetChecks:MonkeyCheckupResponse[],
}