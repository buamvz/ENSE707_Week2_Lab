Test Summary Report
1. Summary
- Testing was completed for the Appointment Booking System after adding the 
appointment cancellation feature. Unit tests were run using MSTest to verify 
both the existing booking functionality and the new cancellation functionality. 
The tests confirmed that the system behaves as expected and meets the specified 
requirements

2. Features Tested
- Appointment booking
- Appointment cancellation
- Doctor available slot updates
- Patient and doctor validation
- Appointment date validation
- Daily booking limits
- Success and error handling

3. Features Not Tested
- Payment processing
- Calendar integration
- Email or SMS notifications
- User login and authentication
- User interface (GUI)

4. Test Environment
- Windows PC
- Visual Studio
- .NET
- MSTest Framework

5. Test Results
Test Area	        | Number of Tests |	Passed  | Failed   | Notes
Booking tests    	| 17	          |17	    | 0	       | Existing booking tests passed
Cancellation tests 	| 5	              |5	    | 0	       | New cancellation feature passed all tests

6. Defects Found
- CancelAppointment() was initially placed outside the AppointmentBookingService 
class, causing compilation errors
- Doctor slots were not initially released after cancellation until the 
ReleaseSlot() method was added

7. Defects Fixed
- Moved CancelAppointment() inside the AppointmentBookingService class
- Added the ReleaseSlot() method to the Doctor class
- Verified that cancelling an appointment correctly marks it as 
cancelled and restores the doctor's available slot

8. Known Issues
- The system does not include payment processing
- Calendar integration has not been implemented
- The application is console/class-based and does not include a user interface

9. Release Recommendation
- All implemented booking and cancellation features passed testing, 
and no major defects remain

10. Lessons Learned
- Writing unit tests early made it easier to find and fix problems
- Regression testing helped confirm that adding the cancellation feature 
did not break the existing booking functionality
- Keeping the code organised and testing each change made debugging much easier