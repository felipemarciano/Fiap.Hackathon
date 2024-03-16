import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICreateVideoRequestModel, IListVideosResponseModel } from '../../Constants/Models/videos.models';
import { BLOB_DOMAIN, INTERNAL_DOMAIN, VIDEO_API_ENDPOINTS } from '../../Constants/Http/video.constants';

@Injectable({
  providedIn: 'root'
})
export class VideoService {

  constructor(private readonly _http: HttpClient) { }

  listVideos(): Observable<IListVideosResponseModel[]>{
    return this._http.get<IListVideosResponseModel[]>(localStorage.getItem('apiUrl')  + VIDEO_API_ENDPOINTS.List);
  }

  createVideo(video: ICreateVideoRequestModel): Observable<any>{
    return this._http.post(localStorage.getItem('apiUrl')  + VIDEO_API_ENDPOINTS.Create, 
      video);
  }

  downloadZip(filePath: string): Observable<any>{
    const headers = { 'responseType': 'blob' as 'json'};
    return this._http.get(localStorage.getItem('blobUrl')  + '/' + filePath, headers);
  }
}
