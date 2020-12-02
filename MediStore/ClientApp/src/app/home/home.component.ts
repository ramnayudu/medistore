import {AfterViewInit, OnInit, Component, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { MediStoreService } from './home.service';

export interface MedicineData {
  id: string;
  medicineName: string;
  brand: string;
  price : number;
  quantity : number;
  expiryDate : string;
  notes: string;
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
export class HomeComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['medicineName', 'brand', 'price', 'quantity','expiryDate'];
  dataSource: MatTableDataSource<MedicineData>;
  allmedicines : MedicineData[];

  @ViewChild(MatPaginator,{static: false}) paginator: MatPaginator;
  @ViewChild(MatSort,{static: false}) sort: MatSort;

  constructor(private mediStoreService: MediStoreService) {
    
  }
  ngOnInit() {
    this.getMedicines();

      // Assign the data to the data source for the table to render
      this.dataSource = new MatTableDataSource(this.allmedicines);
 
  }

  getMedicines(): void {
    this.mediStoreService.getMedicines()
      .subscribe(medicines => (this.allmedicines = medicines));

  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
}


