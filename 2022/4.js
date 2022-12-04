const fs = require("fs");

const { a, b } = fs
  .readFileSync("4.txt")
  .toString()
  .split("\r\n")
  .map((r) => ({
    a: {
      min: parseInt(r.split(",")[0].split("-")[0]),
      max: parseInt(r.split(",")[0].split("-")[1]),
    },
    b: {
      min: parseInt(r.split(",")[1].split("-")[0]),
      max: parseInt(r.split(",")[1].split("-")[1]),
    },
  }))
  .reduce(
    (prev, curr) => {
      if (
        (curr.a.min <= curr.b.min && curr.a.max >= curr.b.max) ||
        (curr.b.min <= curr.a.min && curr.b.max >= curr.a.max)
      ) {
        prev.a++;
      }

      for (let i = curr.a.min; i <= curr.a.max; i++) {
        if (i >= curr.b.min && i <= curr.b.max) {
          return { a: prev.a, b: prev.b + 1 };
        }
      }

      for (let i = curr.b.min; i <= curr.b.max; i++) {
        if (i >= curr.a.min && i <= curr.a.max) {
          return { a: prev.a, b: prev.b + 1 };
        }
      }

      return prev;
    },
    { a: 0, b: 0 }
  );

console.log("4a", a);
console.log("4b", b);
