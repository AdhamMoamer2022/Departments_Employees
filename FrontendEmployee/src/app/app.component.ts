import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { EmployeeService } from './Services/employee.service';
import { Employee } from '../app/Interfaces/employee';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { AddEditComponent } from './Dialogs/add-edit/add-edit.component';
import { ConfirmComponent } from './Dialogs/confirm/confirm.component';
import { ResponseApi } from './Interfaces/response-api';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit, AfterViewInit {
  title = 'FrontendEmployee';

  constructor(
    private employeeService: EmployeeService,
    private snackBar: MatSnackBar,
    private dialog: MatDialog
  ) {}

  displayedColumns: string[] = [
    'FullName',
    'Department',
    'Salary',
    'HireDate',
    'Actions',
  ];
  dataSource = new MatTableDataSource<Employee>();
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  ngOnInit(): void {
    this.showEmployees();
  }
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }

  showEmployees() {
    this.employeeService.getEmployees().subscribe({
      next: (data) => {
        console.log(data);
        if (data.status) this.dataSource.data = data.value;
      },
      error: (e) => {},
    });
  }
  AddNewEmployee() {
    this.dialog
      .open(AddEditComponent, {
        disableClose: true,
        width: '350px',
      })
      .afterClosed()
      .subscribe({
        next: (result) => {
          if (result == 'created') {
            this.showEmployees();
          }
        },
      });
  }
  editEmployee(employee: Employee) {
    debugger;
    this.dialog
      .open(AddEditComponent, {
        disableClose: true,
        width: '350px',
        data: employee,
      })
      .afterClosed()
      .subscribe({
        next: (result) => {
          if (result == 'edited') [this.showEmployees()];
        },
      });
  }
  DeleteEmployee(employee: Employee) {
    this.dialog
      .open(ConfirmComponent, {
        disableClose: true,
        width: '350px',
        data: employee,
      })
      .afterClosed()
      .subscribe({
        next: (response) => {
          if (response == 'deleted') {
            this.employeeService.deleteEmployee(employee.idEmployee).subscribe({
              next: (response: ResponseApi) => {
                if (response.status) {
                  this.snackBar.open('Employee Deleted', 'success', {
                    duration: 3000,
                    horizontalPosition: 'right',
                    verticalPosition: 'top',
                  });
                  this.showEmployees();
                } else {
                  this.snackBar.open('Enable to delete Employee ', 'Error', {
                    duration: 3000,
                    horizontalPosition: 'right',
                    verticalPosition: 'top',
                  });
                }
              },
              error:(e)=>{}
            });
          } else {
          }
        },
        error: (e) => {},
      });
  }
}
