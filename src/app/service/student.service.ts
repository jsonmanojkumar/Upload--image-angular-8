import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class StudentService {

  apiurl :any;
  constructor(private http:HttpClient){

    this.apiurl = `${environment.baseUrl}`;
  }

  post(url: string, body): Observable<any> {   
    var urlStr = this.apiurl + url;
    console.log("testing url",urlStr);
    return this.http.post(urlStr, body);
  }

  get(url:string):Observable<any>{
    console.log("GetData",this.apiurl);
    var urlstr=this.apiurl+url;
    return this.http.get(urlstr);
  }

  put(url: string,data:any):Observable<any>{
    var urlstr=this.apiurl + url;
    return this.http.put(urlstr,data);
  }

}
