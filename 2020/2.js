const fs = require("fs");

const pwds = fs.readFileSync("2.txt").toString().split("\n");

const validPasswords = pwds
  .map((text) => {
    const split = text.split(" ");

    return {
      min: Number(split[0].split("-")[0]),
      max: Number(split[0].split("-")[1]),
      char: split[1].replace(":", ""),
      password: split[2],
    };
  })
  .filter(({ min, max, char, password }) => {
    const count = password.split("").filter((character) => character === char)
      .length;

    return count >= min && count <= max;
  });

console.log("2a", validPasswords.length);

const validPasswords2 = pwds
  .map((text) => {
    const split = text.split(" ");

    return {
      first: Number(split[0].split("-")[0]) - 1,
      second: Number(split[0].split("-")[1]) - 1,
      char: split[1].replace(":", ""),
      password: split[2],
    };
  })
  .filter(
    ({ first, second, char, password }) =>
      (password.split("")[first] === char ||
        password.split("")[second] === char) &&
      password.split("")[first] !== password.split("")[second]
  );

console.log("2b", validPasswords2.length);
