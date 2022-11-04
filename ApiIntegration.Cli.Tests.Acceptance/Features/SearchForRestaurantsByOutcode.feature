Feature: Search for restaurants by outcode
As a user
I want to search for restaurants that deliver at a given outcode

Scenario: Restaurants that deliver at an outcode
    Given the outcode E2
    And the following restaurants that are delivering there
      | Name                   | Rating | CuisineType |
      | The Greek Restaurant   | 4.6    | Greek       |
      | The Turkish Restaurant | 4.1    | Turkish     |
    When a user searches for restaurants at that outcode
    Then the restaurants are returned

Scenario: Produce error when invalid outcode
    Given the outcode E21AA
    And the following restaurants that are delivering there
      | Name                   | Rating | CuisineType |
      | The Greek Restaurant   | 4.6    | Greek       |
      | The Turkish Restaurant | 4.1    | Turkish     |
    When a user searches for restaurants at that outcode
    Then the error "Please provide a valid UK Outcode." is returned