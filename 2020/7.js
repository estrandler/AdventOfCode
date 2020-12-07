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

console.log("A", result.size);

const formattedBags = bags.map((bag) => {
  const children = (bag.match(/[1-9]{1}[^,.]*/g) || []).map((match) => ({
    name: match.split(" ")[1] + " " + match.split(" ")[2],
    count: Number(match.split(" ")[0]),
  }));

  return {
    name: extractBagName(bag),
    children,
  };
});

const countBags = (bagName) => {
  const bag = formattedBags.find(({ name }) => bagName === name);

  if (!bag) return 0;

  const countImmideateChildren = bag.children.reduce(
    (prev, curr) => prev + curr.count,
    0
  );

  const countFromChildren = bag.children
    .map((child) => child.count * countBags(child.name))
    .reduce((prev, curr) => prev + curr, 0);

  return countImmideateChildren + countFromChildren;
};

console.log(countBags("shiny gold"));
