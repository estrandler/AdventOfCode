const fs = require("fs");
let numbers = fs
  .readFileSync("10.txt")
  .toString()
  .split("\r\n")
  .map(Number)
  .sort((a, b) => a - b);

const computePartOne = () => {
  //add 0 and highest + 3
  const numbersPartOne = [0]
    .concat(numbers)
    .concat(numbers[numbers.length - 1] + 3);

  const calculateStepsToNext = (current, nextValue) => {
    const next = nextValue - current;

    return {
      isOneStep: next === 1,
      isTwoSteps: next === 2,
      isThreeSteps: next === 3,
    };
  };

  const counter = numbersPartOne.reduce(
    (prev, curr, index, arr) => {
      const { isOneStep, isTwoSteps, isThreeSteps } = calculateStepsToNext(
        curr,
        arr[index + 1]
      );

      return {
        one: isOneStep ? prev.one + 1 : prev.one,
        three: isThreeSteps ? prev.three + 1 : prev.three,
      };
    },
    {
      one: 0,
      three: 0,
    }
  );

  console.log("A", counter.one * counter.three);
};

const computePartTwo = () => {
  const calculatedNumberOfPaths = numbers.reduce(
    (prev, curr) => {
      const waysIn = prev.filter((x) => curr - x.number <= 3);

      const numberOfPathsSum = waysIn.reduce(
        (prev, x) => prev + x.numberOfPaths,
        0
      );

      return waysIn.concat([{ number: curr, numberOfPaths: numberOfPathsSum }]);
    },
    [{ number: 0, numberOfPaths: 1 }]
  );

  console.log(
    "B",
    calculatedNumberOfPaths[calculatedNumberOfPaths.length - 1].numberOfPaths
  );
};

computePartOne();
computePartTwo();
