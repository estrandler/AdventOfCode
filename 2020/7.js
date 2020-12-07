const fs = require("fs");
const bags = fs.readFileSync("7.txt").toString().split("\r\n");

const extractBagsWhichCanHold = (searchBag) =>
  bags.filter((bag) => bag.indexOf(searchBag) > 0);
const extractBagName = (bag) =>
  bag
    .split(" ")
    .filter((_, index) => [0, 1].includes(index))
    .join(" ");

const firstBags = extractBagsWhichCanHold("shiny gold");

const doStuff = (bag) => {
  const bagName = extractBagName(bag);
  const bags = extractBagsWhichCanHold(bagName);

  return [bags, ...bags.map(doStuff)].flat();
};

const result = new Set([...firstBags.map(doStuff).flat(), ...firstBags]);

console.log(result.size);
