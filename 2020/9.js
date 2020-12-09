const fs = require("fs");
const numbers = fs.readFileSync("9.txt").toString().split("\r\n").map(Number);

const computePartOne = () =>
  numbers.filter((number, index, arr) => {
    const preambleTopIndex = 24;
    if (index <= preambleTopIndex) {
      return false;
    }

    const previous = arr
      .filter(
        (_, innerIndex) =>
          innerIndex < index && innerIndex >= index - preambleTopIndex - 1
      )
      .filter((value, _, innerArr) => innerArr.indexOf(number - value) !== -1)
      .filter((value) => value * 2 !== number);

    return previous.length === 0;
  })[0];

const firstInvalid = computePartOne();

const computePartTwo = () => {
  const longestSequence = numbers
    .reduce((prev, curr, index, arr) => {
      let added = curr;
      const numbersWhichWereAdded = [];
      let i = index;

      while (
        numbersWhichWereAdded.reduce(
          (prevNum, currNum) => prevNum + currNum,
          0
        ) < firstInvalid
      ) {
        numbersWhichWereAdded.push(added);
        added = arr[i + 1];
        i++;
      }

      prev.push(numbersWhichWereAdded);
      return prev;
    }, [])
    .filter(
      (arr) =>
        arr.reduce((prevNum, currNum) => prevNum + currNum, 0) ===
          firstInvalid && arr.length > 1
    )[0]
    .sort((a, b) => b - a);

    return longestSequence[0] + longestSequence[longestSequence.length - 1]
};

console.log("A", computePartOne());
console.log("B", computePartTwo());
