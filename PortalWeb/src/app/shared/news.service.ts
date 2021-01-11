import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { News } from './message-detail.model';

@Injectable({
  providedIn: 'root'
})
export class NewsService {

  readonly rootURL = "http://localhost:5002/api";

  constructor(private http: HttpClient) { }
  news: News[];
  
  getNews(){
    return this.http.get(this.rootURL + "/News");
  }

  postNews(news) {
    return this.http.post(this.rootURL + "/News/insert", news);
  }

  editNews(news: News){
    return this.http.put(this.rootURL + "/News/update", news);
  }

  refreshNewsList() {
    return this.http
      .get<any>(this.rootURL + "/News")
      .toPromise()
      .then(res => (this.news = res as News[]));
  }

}
