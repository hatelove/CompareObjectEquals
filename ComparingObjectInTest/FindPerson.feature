Feature: FindPerson	


Scenario: Compare 2 Person instance Equals
	When I got a acutal person
	| Id | Name | Age |
	| 1  | A    | 10  |
	Then I hope actual person should be equal to expected person
	| Id | Name | Age |
	| 1  | A    | 10  |
