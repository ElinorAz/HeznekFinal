import { ScholarshipModel } from "../../models/scholarship/scholarship.model";

export interface StudentScholarshipModel {
    january: number,
    february: number,
    march: number,
    april: number,
    may: number,
    june: number,
    july: number,
    august: number,
    september: number,
    october: number,
    november: number,
    december: number,
    scholarship: ScholarshipModel,
    profileId?,
    total?: number,
    id: number
}