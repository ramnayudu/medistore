import { OnInit, Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MediStoreService } from './home.service';

export interface MedicineData {
  id: string;
  medicineName: string;
  brand: string;
  price: number;
  quantity: number;
  expiryDate: Date;
  notes: string;
  color:string;
}

/** Constants used to fill up our data base. */
const COLORS: string[] = [
  'red', 'yellow'
];

/**
 * @title Data table with sorting, pagination, and filtering.
 */
@Component({
  styleUrls: ['home.component.css'],
  templateUrl: 'home.component.html',
  providers: [MediStoreService]
})
export class HomeComponent implements OnInit {
  displayedColumns: string[] = ['medicineName', 'brand', 'price', 'quantity', 'expiryDate'];
  dataSource: MatTableDataSource<MedicineData>;
  allmedicines: MedicineData[];

  constructor(private mediStoreService: MediStoreService) {

  }
  ngOnInit() {
    this.getMedicines();

  }

  getMedicines(): void {
    this.mediStoreService.getMedicines()
      .subscribe((data: MedicineData[]) => {
        this.allmedicines = data;
        this.allmedicines.forEach(element => {

          if(element.quantity <= 10 )
          {
            element.color = "yellow"
          }
         if (this.calculateDiff(element.expiryDate) < 30 )
         {
           element.color = "red"
         }
          
        });
        this.dataSource = new MatTableDataSource(this.allmedicines);
      });

  }

  calculateDiff(dateSent){
    let currentDate = new Date();
    dateSent = new Date(dateSent);
    return Math.floor((Date.UTC(dateSent.getFullYear(), dateSent.getMonth(), dateSent.getDate()) - Date.UTC(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate()) ) /(1000 * 60 * 60 * 24));
}


  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}


