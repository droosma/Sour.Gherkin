Feature: Scenario Outline

    Scenario Outline: Eating
      Given there are <start> cucumbers
      When I eat <eat> cucumbers
      Then I should have <left> cucumbers

      Examples:
        | start    | eat | left |
        |  1200000 |  5  |  7   |
        |  20      |  5  |  15  |