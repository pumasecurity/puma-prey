import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'datePipe'
})
export class DatePipePipe implements PipeTransform {

  transform(date: Date, ...args: unknown[]): string {
    
   var currentDate= date.toString() ;
    currentDate= currentDate.split('-')[1];
    console.log(date.toString())
    return currentDate;
  }

}
