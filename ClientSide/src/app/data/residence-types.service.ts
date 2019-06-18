import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ResidenceTypesService {

  public ResidenceTypes = [
    {id:0, value: "מעונות"},
    {id:1, value: "בית ההורים"},
    {id:2, value: "דירה שכורה"}
  ];
  constructor() { }
}
