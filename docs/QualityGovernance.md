## Process Assurance vs Product Assurance


| Area        | Process Assurance												  | Product Assurance
|-------------|-------------------------------------------------------------------|----------------------------------
| Main Focus  | How the work is performed										  | Quality of the software product
| Example     | Requierment review, coding stanards, Git comments, test processs  | Validation logic, working booking feature, passing tests
| Evidence	  | Review checklist, commits, test plan, CI results				  | Test results, defect reports, working prototype
| Goal	      | Prevent quality problems										  | Detect and confirm product quality
|			  |																	  |

## Why both are needed
Process assurance stops defects from being created by making sure work is done in a controlled, reviwed, tested
way. But a good process cannot guarantee a perfect product, so we still need product assurance to test the actual
software and confrim it behaves correctly. One prevents the other proves.

Quality Governance Rules:

| GovernanceArea  | Rule												              | Evidence
|-----------------|-------------------------------------------------------------------|----------------------------------
| Requirements    | Each new feature must have at least one requirement ID			  | Requirements list
|-----------------|-------------------------------------------------------------------|----------------------------------
| Testing         | Each requirement must have at least one test case                 | Traceability matrix
|-----------------|-------------------------------------------------------------------|----------------------------------
| Code quality    | Code must pass all MStests before commit            	   		  | Test results
|-----------------|-------------------------------------------------------------------|----------------------------------
| GitHub          | Each student must commit meaningful work regularly				  | Git history
|-----------------|-------------------------------------------------------------------|----------------------------------
| AI use          | Copilot suggestions must be reviewed and tested 				  | AI reflection notes
|-----------------|-------------------------------------------------------------------|----------------------------------
| Defect          | Defects must be recorded with status and severity				  | Defects log
|-----------------|-------------------------------------------------------------------|----------------------------------
| Release         | Release A feature can only be released if exit criteria are met	  | Test summary report

These quality governance rules help ensure the Appointment Booking System is 
developed in a consistent and reliable way. By linking requirements to tests, 
running unit tests before committing code, recording defects, and following the 
defined release criteria, the project maintains a higher level of quality. Regular Git 
commits and reviewing AI-generated code also help improve traceability, accountability, and 
confidence that the software works as expected before release
This matches the documents you've already produced (requirements, test plan, 
test strategy, test summary report, Git history, and AI reflection) and is appropriate 
for the scope of your assignment

//this is a sameple defect log table
| Defect ID| Description                                             | Severity | Status | Found In           | Fixed In
|----------|---------------------------------------------------------|----------|--------|--------------------|----------
| DEF-001  | Example: Slot count did not increase after cancellation | High     | Fixed  | Cancellation tests | Updated CancelAppointment method

