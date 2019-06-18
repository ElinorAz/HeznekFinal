export interface EventModel {
    id: number;
    name: string;
    subject: string;
    date: Date;
    time: string;
    location: string;
    expected: number;
    participantTypes: number[];
    attending?;
    notAttending?;
}