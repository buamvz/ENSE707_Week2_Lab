Feature under test: Appointment Cancellation

Test Objective:
The objective of this test plan is to verify that appointments can be cancelled correctly, 
the doctor's available slot is restored, and invalid cancellations are handled correctly

Requirements to be Tested
- REQ-CAN-01: The system shall allow an existing appointment to be cancelled
- REQ-CAN-02: When an appointment is cancelled, the doctor's available slot count 
shall increase by one
- REQ-CAN-03: The system shall not allow cancellation of an appointment that does not exist

Test Items - following classes and methods will be tested:
- AppointmentBookingService
- Doctor
- AppointmentRequest
- Appointment cancellation method (new)

Test Approach - testing will mainly use unit testing with MSTest
Tests will verify:
- Cancelling an existing appointment succeeds
- Doctor's available slots increase after cancellation
- Cancelling a non-existent appointment fails
- The correct success or error message is returned
- Existing booking functionality still works after the new feature is added (regression testing)

Test Data
- Test Case: Valid cancellation Data: Existing appointment for Dr Mark with available booking
- Test Case: Invalid cancellation Data: Appointment that does not exist
- Test Case: Doctor Data: Dr Mark
- Test Case: Patient Data: Diana William/Aroha
- Test Case: Appointment Date Data: Tomorrow's date

Responsibilities
- Role: Developer Responsibility: Implement the cancellation feature
- Role: Developer Responsibility: Write and run MSTest unit tests
- Role: Developer/tester Responsibility: Verify test results and fix any defects

Schedule
Task: Implement cancellation feature Time: Week 1
Task: Write unit tests Time: Week 2
Task: Run tests and fix defects Time: Week 3
Task: Final regression testing Time: Week 4

Passing/Fail Criteria
Pass criteria
- All cancellation tests pass
- Doctor's available slot increases after a successful cancellation
- Invalid cancellations are rejected
- Existing booking tests continue to pass
Fail criteria
- Any cancellation test fails.
- Doctor's available slots are not updated correctly
- The system allows cancellation of a non-existent appointment
- Existing booking functionality breaks after adding the feature

Risks
- Risk: Available slots are not updated correctly Mitigation: Create unit tests to verify 
slot count before and after cancellation
- Risk: Cancelling an appointment that does not exist Mitigation: Validate the appointment 
exists before cancelling
- Risk: New feature breaks existing booking functionality Mitigation: Run all existing 
MSTest tests as regression tests after implementing the feature
