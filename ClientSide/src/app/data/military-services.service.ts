import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MilitaryServicesService {

  public MilitaryServices = [
    {id:0, value: "שירות צבאי מלא"},
    {id:1, value: "שירות צבאי חלקי"},
    {id:2, value: "שירות לאומי מלא"},
    {id:3, value: "שירות לאומי חלקי"},
    {id:4, value: "ללא שירות"},
  ];
  constructor() { }
}
