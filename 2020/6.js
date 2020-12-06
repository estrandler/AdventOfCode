const fs = require("fs");
const groups = fs
  .readFileSync("6.txt")
  .toString()
  .split("\r\n")
  .reduce(
    (prev, curr) => {
      if (curr === "") {
        prev.unshift({ text: "", memberCount: 0 });
      } else {
        prev[0].text += curr;
        prev[0].memberCount++;
      }

      return prev;
    },
    [{ text: "", memberCount: 0 }]
  );

const A = groups
  .map(({ text }) => new Set(text))
  .reduce((prev, curr) => prev + curr.size, 0);

const B = groups
  .map(
    ({ text, memberCount }) =>
      [...new Set(text)].filter(
        (character) =>
          text.split("").filter((groupChar) => groupChar === character)
            .length === memberCount
      ).length
  )
  .reduce((prev, curr) => prev + curr, 0);

console.log(A);
console.log(B);
