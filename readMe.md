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

* 'RegistrationsController.GetRegistrationById'	.../api/registartions/getRegistrationById	
* `RegistrationsController.GetTimeslots`	.../api/registrations/getTimeslots
* `VaccinationsController.StoreVaccination`	  .../api/vaccinations/storeVaccination

* Ohter helper methods (used: database transactions):
  * `ImportRegistrations`: Import registrations from a JSON file
  * `DeleteEverything`: Delete everything (registrations, vaccinations);



(screen1.png)
(screen2.png)
(screen3.png)
(screen4.png)

