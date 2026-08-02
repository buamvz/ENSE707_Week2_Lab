Test Strategy
1. Purpose - The purpose of this Test Stratgery is to define a approach, scpoe, levels
environment, tools, defect-management and entry/exit criteria for validating the Appointment 
Booking System. The goal is to ensure all functions are correct before a release.

2. Scope of Testing
- Booking an appinments (create, read, update, cancel)
- docotr avalibilty and availabile slots
- Patient and doctor validation
- Appointment date validation
- Daily booking limits
- Success and failure messages

3. Out of Scope
-  Payment processing
- Calendar integration
- Email or SMS notifications
- User login and authentication
- Mobile or web interface

4. Test Levels 
- Unit testing
	- Scpoe: individual functions, business logic and utilites
	- Owner: develoers
	- Frequencey: Whenever code changes
	- Tools: MSTest
- Intergration testing
	- Scpoe: interaction between components (API controllers - services - repositories; 
calendar/payment integration stubs)
	- Owner: developers
	- Frequencey: After new features are added
- Regression testing
	- Scpoe: Re-run existing tests after code changes to make sure nothing has broken
	- Owner: develoers
	- Frequencey: After each update
- Validation testing
	- Scpoe: Check that the program meets the assignment requirements and business rules
	- Owner: develoers
	- Frequencey: Before final submission

5. Test Types 
- Unit Testing – Tests individual methods and classes
- Integration Testing – Tests how different classes work together
- Regression Testing – Makes sure previous features still work after changes
- Validation Testing – Checks that booking rules are followed (no past dates, available slots, 
booking limits)

6. Test Environment - Windows PC, Visual Studio, .NET, MSTest Framework

7. Tools - Visual Studio, MSTest, Git, GitHub

8. Defect Management Approach 
- Bugs are found by running MSTest unit tests
- If a test fails, the code is fixed and the test is run again
- New tests are added when new features or bugs are introduced

9. Entry Criteria - testing begins when
- The project builds successfully
- The classes compile without errors
- Test cases have been written

10. Exit Criteria testing is completed when
- All MSTest tests pass
- No major bugs remain
- All assignment requirements have ben tested

11. Risks and Mitigation 
- Risk: Invalid patient or doctor data - Mitigation: Validate inputs and use unit tests
- Risk: Booking when no slots are available - Mitigation: Test available slot checks
- Risk: Booking appointments in the past - Mitigation: Validate appointment dates with tests
- Risk: Doctor exceeds daily booking limit - Mitigation: Test the maximum daily appointment rule
- Risk: Changes break existing features - Mitigation: Run regression tests after code changes

- Patient information/data privacy and compliance - Mitigation: anonymize test data, run security scans and audits
- Race conditions and concurrency - Mitigation: stress test (for double booking) and validate in intergration test

