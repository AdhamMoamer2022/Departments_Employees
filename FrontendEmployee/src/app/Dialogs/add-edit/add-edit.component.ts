import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import * as moment from 'moment';
import { Department } from 'src/app/Interfaces/department';
import { Employee } from 'src/app/Interfaces/employee';
import { ResponseApi } from 'src/app/Interfaces/response-api';
import { DepartmentService } from 'src/app/Services/department.service';
import { EmployeeService } from 'src/app/Services/employee.service';

@Component({
  selector: 'app-add-edit',
  templateUrl: './add-edit.component.html',
  styleUrls: ['./add-edit.component.css'],
})
export class AddEditComponent implements OnInit {
  action: string = 'Add';
  actionButton: string = 'Save';
  formEmployee: FormGroup;
  departmentList: Department[] = [];

  constructor(
    private fb: FormBuilder,
    private employeeService: EmployeeService,
    private departmentService: DepartmentService,
    private snackBar: MatSnackBar,
    private dialogRef: MatDialogRef<AddEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Employee
  ) {
    this.formEmployee = this.fb.group({
      fullName: ['', Validators.required],
      idDepartment: [0, Validators.required],
      salary: ['', Validators.required],
      hireDate: ['', Validators.required],
    });

    this.departmentService.getDepartments().subscribe({
      next: (response: ResponseApi) => {
        if (response.status) this.departmentList = response.value;
      },
      error: (e: any) => {
        console.log(e);
      },
    });
  }

  ngOnInit(): void {
    if (this.data != null) {
      debugger;
      this.formEmployee.patchValue({
        fullName: this.data.fullName,
        idDepartment: this.data.idDepartment,
        salary: this.data.salary,
        hireDate: moment(this.data.hireDate).format('DD/MM/YYYY'),
      });
      this.action = 'Edit';
      this.actionButton = 'Update';
    }
  }

  showAlert(msg: string, title: string) {
    this.snackBar.open(msg, title, {
      duration: 3000,
      horizontalPosition: 'right',
      verticalPosition: 'top',
    });
  }

  addEditEmployee() {
    const model: Employee = {
      idEmployee: this.data == null ? 0 : this.data.idEmployee,
      fullName: this.formEmployee.value.fullName,
      idDepartment: this.formEmployee.value.idDepartment,
      salary: this.formEmployee.value.salary,
      hireDate: moment(this.formEmployee.value.hireDate).format('DD/MM/YYYY'),
    };

    if (this.data == null) {
      this.employeeService.AddEmployee(model).subscribe({
        next: (response: ResponseApi) => {
          if (response.status) {
            this.showAlert('Employee Created', 'success');
            this.dialogRef.close('created');
          } else {
            this.showAlert('Enable to create Employee', 'Error');
          }
        },
        error: (e) => {},
      });
    } else {
      this.employeeService.updateEmployee(model).subscribe({
        next: (response: ResponseApi) => {
          if (response.status) {
            this.showAlert('Employee Edited', 'success');
            this.dialogRef.close('edited');
          } else {
            this.showAlert('Enable to Edit the Employee', 'Error');
          }
        },
        error: (e) => {},
      });
    }
  }
}
