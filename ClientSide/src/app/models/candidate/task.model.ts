export interface TaskModel
{
    id: number;
    name: string;
    fileName: string;
    lastUpdated: Date;
    file: File;
    link?: string;
    downloadName?: string;
}