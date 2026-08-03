Continuous Improvement


- What Worked Well
Writing unit tests while developing the Appointment Booking System helped find 
problems early. The test strategy, test plan, and test cases made it easier to check 
that the booking and cancellation features worked correctly. Regular Git commits also helped 
keep track of changes throughout the project

- What Did Not Work Well
Some code needed to be changed after new features were added. For example, updating the 
booking service to support appointment cancellation required changes to existing methods and 
tests. This caused some errors that had to be fixed before all tests passed

- Root Cause of One Issue
One issue occurred because the CancelAppointment() method was placed outside the 
AppointmentBookingService class. This caused compilation errors until the method was moved 
into the correct location

- Improvement Action
Before adding new features, I will review the class structure and plan how the new 
code will fit into the existing project. I will also update the related unit tests at the 
same time as the code to make sure everything continues to work

- How We Will Check the Improvement
The improvement will be checked by running all MSTest unit tests after each change. 
If all existing and new tests pass, it shows that the changes have not introduced new defects. 
Regular code reviews and Git commits will also help identify problems earlier

- Quality Culture Reflection
This project showed that reviewing code and writing tests early helps prevent 
defects before they become bigger problems. Regular Git commits improved visibility by 
showing the progress of the project and making it easier to track changes. The test results 
provided evidence that the booking and cancellation features met the project requirements
and were ready for demonstration. Teamwork also supports quality because team members can 
review each other's work, share ideas, and help identify defects that might otherwise be 
missed. In the next lab or project phase, I will improve my process by planning new features 
before coding, committing changes more regularly, and writing unit tests alongside the 
implementation to catch issues earlier


| Practice                | How It Could Be Used in This Project												  
|-------------------------|------------------------------------------------------------------
| Sprint planning         | Plan a small number of features to complete each week, such as appointment booking, cancellation, and writing unit tests								  
| Daily stand-up          | Discuss progress, any coding or testing problems, and what work will be completed next
| Definition of Done 	  | A feature is complete only when the code is finished, reviewed, unit tested, documented, and all tests pass		  
| Continuous Integration  | Run MSTest unit tests automatically whenever code is committed or pushed to the repository to check for errors								  								 
| Regression testing	  | Re-run all existing booking and cancellation tests after making changes to ensure no existing features have broken									  								 
| Retrospective	          | At the end of each lab or project phase, review what worked well, what problems occurred, and how the development and testing process can be improved for the next sprint

Ask Copilot for QA Process Suggestions

Lightweight test strategy (practical for a small student project)
•	Unit tests (fast, isolated): cover domain rules (booking, cancellation, slot counts, validation). Mock or inject collaborators (clock, repository) where needed.
•	Small integration tests: verify interactions end-to-end using in-memory repositories (persistence + service).
•	Concurrency tests (one or two): simulate parallel bookings/cancellations for critical state (slots).
•	Property / boundary tests: validate invariants (AvailableSlots >= 0, daily bookings <= MaxDailyAppointments).
•	Test pyramid: ~70% unit, 20% integration, 10% concurrency/edge.
•	Automation: run tests on every PR via CI (GitHub Actions) and fail build on regressions.
Quality governance checklist (student project, lightweight)
•	Code review required for all PRs (peer review + one approver).
•	CI must run: restore, build, unit tests, static analyzer (nullable + Roslyn analyzers).
•	Keep public API documented (XML docstub) for main service methods.
•	Enforce consistent style: editorconfig, nullable enabled, implicit usings as project default.
•	Maintain minimal test coverage threshold for core domain (e.g., booking/cancellation logic).
•	Track known issues/technical debt in CHANGELOG or ISSUE board.
•	Security basics: validate inputs, avoid leaking PII in logs/messages.
•	Release checklist for demo: build, tests green, README updated, sample data script.
MSTest cases for appointment cancellation and slot-release behavior (Each is a test method name + one-line Arrange/Act/Assert)
•	CancelAppointment_ExistingAppointment_MarksAppointmentAsCancelledAndReleasesSlot
•	Arrange: create doctor (AvailableSlots=1), create appointment for that doctor, service.BookAppointment(...).
•	Act: service.CancelAppointment(appointment).
•	Assert: appointment.IsCancelled == true AND doctor.AvailableSlots increased by 1 AND daily booking count decreased.
•	CancelAppointment_NullAppointment_ThrowsArgumentNullException
•	Arrange: service instance.
•	Act/Assert: Assert.ThrowsException<ArgumentNullException>(() => service.CancelAppointment(null));
•	CancelAppointment_AlreadyCancelled_ThrowsOrReturnsFalse (choose contract)
•	Arrange: cancel once, then attempt to cancel again.
•	Act: attempt second cancel.
•	Assert: either Assert.ThrowsException<InvalidOperationException>(...) OR Assert.IsFalse(service.CancelAppointment(...)) depending on API decision (idempotent vs exception).
•	CancelAppointment_ReleasesSpecificDateSlot
•	Arrange: book two appointments on different dates with same doctor and MaxDailyAppointments limit >1.
•	Act: cancel the appointment for DateA.
•	Assert: the daily bookings counter for DateA decremented; AvailableSlots adjusted only once.
•	CancelAppointment_ConcurrentCancels_AreHandledSafely
•	Arrange: book appointment, then run two concurrent CancelAppointment calls (Task.Run).
•	Act/Assert: service behaves safely (only one successful cancellation) and AvailableSlots final value consistent.
•	CancelAppointment_ReleaseSlotFailure_CompensatesOrSignalsError
•	Arrange: inject a mocked doctor/repository that throws on ReleaseSlot.
•	Act: cancel
•	Assert: either appointment remains cancelled but system logs/fails gracefully, or cancel is rolled back — depending on chosen transactional contract.
Review of the current Cancel flow (reliability, maintainability, testability) Current code excerpts (observed)
•	AppointmentBookingService.CancelAppointment(appointment)
•	Throws ArgumentNullException for null appointment.
•	Calls appointment.Cancel(); then appointment.Doctor.ReleaseSlot();
•	Appointment.Cancel()
•	Throws InvalidOperationException if already cancelled; otherwise sets IsCancelled = true.
•	Doctor.ReleaseSlot() implementation issues observed (see file):
•	It appears misplaced/outside class braces in current file -> compile error.
•	ReleaseSlot() increments AvailableSlots but does NOT decrement the doctor's per-date daily booking counter ( _dailyBookings ), so daily limit invariants can break.
•	ReserveSlots and daily bookings are not protected by synchronization (locks) — race conditions possible under concurrent bookings/cancels.
Reliability concerns
•	Non-transactional sequence: appointment.Cancel() then ReleaseSlot(). If ReleaseSlot fails (exception, persistence error), the appointment is left cancelled but slot not released -> inconsistent state. Need transactional/compensating behavior.
•	Concurrency: ReserveSlots and ReleaseSlot mutate shared state without locks; concurrent operations can lead to negative or over-counts.
•	Idempotency: Cancel throws for repeated cancels, forcing callers to handle exceptions. Consider an idempotent cancel that returns a boolean/result instead.
Maintainability issues
•	Namespace mismatch: Appointment is in ENSE707_AppointmentBooking while others are in AppointmentBooking -> confusing and may cause test/reference issues.
•	ReleaseSlot code appears outside class (syntax error) — makes project uncompilable.
•	Business logic spread across domain objects (Doctor keeps date counters) — better separated into an AppointmentRepository or SchedulingService for clarity and persistence.
•	Messages and exceptions are inconsistent (some methods return BookingResult earlier; now BookAppointment returns Appointment).
Testability issues
•	AppointmentBookingService directly manipulates Doctor and Appointment concrete types — hard to mock or simulate failures (e.g., failing ReleaseSlot).
•	Time access uses DateTime.Today directly — harder to test notice/booking window. Inject an IClock or use DateOnly with a testable provider.
•	No interfaces for persistence or scheduling: introduce IDoctorSchedule or ISchedulingRepository to simulate DB failures in tests.
Recommended fixes (minimal, prioritized)
1.	Fix compile errors: place ReleaseSlot inside Doctor class and remove extra braces; unify namespaces to AppointmentBooking for all domain types.
2.	Make ReleaseSlot accept a date (ReleaseSlot(DateTime date)) and decrement _dailyBookings[date.Date] safely; ensure AvailableSlots and per-day counters remain consistent.
3.	Add synchronization (lock or Interlocked) around ReserveSlots/ReleaseSlot and daily counters, or use a concurrent collection.
4.	Make CancelAppointment transactional/compensating:
•	Option A (preferred): attempt to release the slot first; if success then mark appointment cancelled OR use repository transaction so both succeed or both fail.
•	Option B: mark cancelled, try release, if release fails then roll back IsCancelled and throw a specific exception (and log).
5.	Consider making Appointment.Cancel idempotent and return a boolean (true if changed, false if already cancelled) so callers do not need to catch exceptions for expected repeated operations.
6.	Introduce interfaces for clock and persistence to make tests deterministic and to allow injection of failure scenarios for reliability tests.
Example API adjustments (conceptual)
•	Doctor.ReserveSlot(DateTime date) / Doctor.ReleaseSlot(DateTime date)
•	AppointmentBookingService.CancelAppointment(Appointment appointment) => bool or Result object with Code/Message
•	Inject IClock into service for DateTime.Today behavior
If you want, I can:
•	1.	Fix the compile error + namespace mismatch + implement ReleaseSlot(DateTime) and synchronization, or
•	2.	Change CancelAppointment to an idempotent bool-result and add unit tests for the cases above.
