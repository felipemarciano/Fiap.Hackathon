export interface IListVideosResponseModel{
    name: string;
    startDate: string;
    startProcessingDate: string;
    endProcessingDate: string;
    status: string;
    filePath: string;
}

export interface ICreateVideoRequestModel{
    name: string;
    base64Video: string;
}