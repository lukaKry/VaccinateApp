# VaccinateApp

## Introduction

A simple website for people who want to register for a vaccination.

Each user who wants to register must type *Social Security Number* (short *SSN*) and their PIN.

If SSN and PIN are correct, he can *pick a date*. The vaccinations availability is *between January 18th and 20th 2021*.

Once user picked a date, he can select a *time slot*. 


## Data Model

Database contains two tables *registrations* and *vaccinations*.

* *Registration* properties:
  * Social Security Number
  * PIN code
  * First name
  * Last name
  * Optional reference to the vaccination appointment, if the patient has already created one.
* *Vaccinations* properties:
  * Vaccination appointment's date and time
  * Mandatory reference to the registration that the appointment is based on. 


## API endpoints

* `RegistrationsController.GetRegistrationById`	.../api/registartions/getRegistrationById	
* `RegistrationsController.GetTimeslots`	.../api/registrations/getTimeslots
* `VaccinationsController.StoreVaccination`	  .../api/vaccinations/storeVaccination

* Ohter helper methods (used: database transactions):
  * `ImportRegistrations`: Import registrations from a JSON file
  * `DeleteEverything`: Delete everything (registrations, vaccinations);



![Screenshot1](screen1.png)
![Screenshot2](screen2.png)
![Screenshot3](screen3.png)
![Screenshot4](screen4.png)

