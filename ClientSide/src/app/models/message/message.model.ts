export interface MessageModel {
    topic: string;
    content: string;
    file: File;
    sendtowho: number[];
}