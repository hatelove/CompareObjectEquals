Feature: FindPerson	


Scenario: Compare 2 Person instance Equals
	When I got a acutal person
	| Id | Name | Age |
	| 1  | A    | 10  |
	Then I hope actual person should be equal to expected person
	| Id | Name | Age |
	| 1  | A    | 10  |

	Scenario: Compare 2 Person Collection Equals
	When I got a actual person collection
	| Id | Name | Age |
	| 1  | A    | 10  |
	| 2  | B    | 20  |
	| 3  | C    | 30  |
	Then I hope actual person collection should be equal to expected person collection
	| Id | Name | Age |
	| 1  | A    | 10  |
	| 2  | B    | 20  |
	| 3  | C    | 30  |