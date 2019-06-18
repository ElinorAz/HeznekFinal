import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DegreeTypesService {
  public DegreeTypes = [
    {id:0, value: "תואר ראשון"},
    {id:1, value: "תואר שני"},
    {id:2, value: "תואר שלישי"}
  ];
  constructor() { }
}
