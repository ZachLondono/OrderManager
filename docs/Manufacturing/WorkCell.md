# WorkCell
Represents a list of items which need to be manufactured
> public class Job

## Fields
|	Name				|	Type					|	Summary
| ---------             | ---------                 | ---------
| _jobs					| List<Job>					| List of jobs scheduled for this work cell

## Properties
|	Name				|	Type					|	Summary
| ---------             | ---------                 | ---------
| Id					| int						| ID of the work cell
| Alias 				| string					| Name of the work cell
| ProductId				| int						| Id of the product
| ExpectedMaxOutput		| int						| Expected maximum output of the work cell for the period
| ActiveJobs			| IReadOnlyCollection<Job\> | List of jobs scheduled for this work cell 

## Methods
|	Name				|	Value				|	Summary
| ---------             | ---------             | ---------
| ScheduleJob(Job)		| void					| 
| RemoveJob(Job) 		| void					| 
