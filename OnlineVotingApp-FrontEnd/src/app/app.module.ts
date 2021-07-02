import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './pages/login/login.component';
import { routing } from './app-routing.module';
import { HomeComponent } from './pages/home/home.component';
import { ForgotPasswordComponent } from './pages/forgot-password/forgot-password.component';
import {MatFormFieldModule} from '@angular/material/form-field';
import { NavBarComponent } from './shared/components/nav-bar/nav-bar.component';
import { ChangePasswordComponent } from './pages/change-password/change-password.component';
import { AlertComponent } from './shared/components/alert/alert.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AccountActivationComponent } from './pages/activate-account/activate-account.component';
import { UserView } from './pages/user-view/user-view.component';
import { CollegeComponent } from './pages/colleges/colleges.component';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ChartsModule, MDBBootstrapModule } from 'angular-bootstrap-md';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; // If You need animations
import { MatTabsModule } from '@angular/material/tabs';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatListModule, MatSelectionList } from '@angular/material/list';
import { UsersComponent } from './pages/users/users.component';
import { MatCheckbox, MatCheckboxModule } from '@angular/material/checkbox';
import { MatSelect, MatSelectModule } from '@angular/material/select';
import {MatTableModule} from '@angular/material/table';
import { AddUser } from './pages/users/add-dialog/add-dialog.component';
import {MatDialogModule} from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { UpdateUser } from './pages/users/update-dialog/update-dialog.component';
import { AddCollegeDialog } from './pages/colleges/add-college/add-college.component';
import { UpdateCollegeDialog } from './pages/colleges/update-college/update-college.component';
import { UpdateDepartmentDialog } from './pages/colleges/update-department/update-department.component';
import { AddDepartmentDialog } from './pages/colleges/add-department/add-department.component';
import { ElectoralRoomComponent } from './pages/elections/elections.component';
import { AddCandidateDialog } from './pages/elections/add-candidate/add-candidate.component';
import { AddElectoralRoomDialog } from './pages/elections/add-electoral-room/add-electoral-room.component';
import { UpdateElectoralRoomDialog } from './pages/elections/update-electoral-room/update-electoral-room.component';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import {MatIconModule} from '@angular/material/icon';
import {MatMenuModule} from '@angular/material/menu';
import{MatPaginatorModule} from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatToolbarModule } from '@angular/material/toolbar';
import { ConfirmDeleteUsers } from './pages/users/confirm-delete-users/confirm-delete-user.component';
import { ConfirmDeleteDepartment } from './pages/colleges/confirm-delete-department/confirm-delete-department.component';
import { ConfirmDeleteCollege } from './pages/colleges/confirm-delete-college/confirm-delete-college.component';
import { ConfirmDeleteCandidate } from './pages/elections/confirm-delete-candidate/confirm-delete-candidate.component';
import { ConfirmDeleteElectoralRoom } from './pages/elections/confirm-delete-electoral-room/confirm-delete-electoral-room.component';
import { UserViewVote } from './pages/user-view/user-view-vote-dialog/user-view-vote-dialog';
import { ResultsDialog } from './pages/user-view/results-dialog/results-dialog';

@NgModule({
  
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbModule,
    routing,
    BrowserAnimationsModule,
    MDBBootstrapModule,
    MatTabsModule,
    MatExpansionModule,
    MatListModule,
    FormsModule,
    MatCheckboxModule,
    MatSelectModule,
    MatTableModule,
    MatFormFieldModule,
    MatDialogModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatIconModule,
    MatMenuModule,
    MatPaginatorModule,
    MatSortModule,
    MatToolbarModule,
    ChartsModule
  ],
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    ForgotPasswordComponent,
    AccountActivationComponent,
    NavBarComponent,
    ChangePasswordComponent,
    AlertComponent,
    UserView,
    CollegeComponent,
    ElectoralRoomComponent,
    UsersComponent,
    AddUser,
    UpdateUser,
    AddCollegeDialog,
    UpdateCollegeDialog,
    UpdateDepartmentDialog,
    AddDepartmentDialog,
    AddCandidateDialog,
    AddElectoralRoomDialog,
    UpdateElectoralRoomDialog,
    ConfirmDeleteUsers,
    ConfirmDeleteDepartment,
    ConfirmDeleteCollege,
    ConfirmDeleteCandidate,
    ConfirmDeleteElectoralRoom,
    UserViewVote,
    ResultsDialog
  ],
  providers: [],
  bootstrap: [AppComponent],
  schemas:[NO_ERRORS_SCHEMA]
})
export class AppModule { }
