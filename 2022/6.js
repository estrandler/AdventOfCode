const fs = require("fs");

const input = fs.readFileSync("6.txt").toString();

const getLastProcessedInString = (inputString, numberToFind) => {
  const marker = [];
  for (let i = 0; i < inputString.length; i++) {
    if (
      marker.length === numberToFind &&
      marker.every(
        (c) => marker.filter(({ char }) => char === c.char).length === 1
      )
    ) {
      break;
    }

    if (marker.length === numberToFind) {
      marker.shift();
    }

    marker.push({ i, char: inputString[i] });
  }

  return marker.pop().i + 1;
};

console.log("6a", getLastProcessedInString(input, 4));
console.log("6b", getLastProcessedInString(input, 14));
