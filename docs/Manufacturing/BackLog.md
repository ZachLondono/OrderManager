# BackLog
Represents all the jobs with a specific product which have not yet  been scheduled for production
> public class BackLog

## Fields
|	Name	|	Type			 |	Summary
| --------- | ---------          | ---------
| _jobs		| List\<Job\> | List of jobs in the back log

## Properties 
|	Name	|	Type					  |	Summary
| --------- | ---------                   | ---------
| Id 		| int 						  | The ID of the back log
| ProductId | int						  | The ID of the product this back log is associated with
| Jobs		| IReadOnlyList\<Job> | List of jobs in the back log

## Methods
|	Name	    	|	Value	|	Summary
| ---------         | --------- | ---------
| AddJob(Job) 		| void		| Adds a job to the back log
| RemoveJob(Job)	| void		| Removes a job from the back log
