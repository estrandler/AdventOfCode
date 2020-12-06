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
  .map(({ text }) => String.prototype.concat(...new Set(text)))
  .reduce((prev, curr) => prev + curr.length, 0);

const B = groups
  .map((group) => {
    const all = String.prototype.concat(...new Set(group.text)).split("");
    return all.filter(
      (character) =>
        group.text.split("").filter((groupChar) => groupChar === character)
          .length === group.memberCount
    ).length;
  })
  .reduce((prev, curr) => prev + curr, 0);

console.log(A);
console.log(B);
