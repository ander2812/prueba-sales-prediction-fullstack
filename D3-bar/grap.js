document.addEventListener("DOMContentLoaded", () => {
  const sourceDataInput = document.getElementById("sourceData");
  const updateDataButton = document.getElementById("updateData");
  const chartContainer = document.querySelector(".chart-container");

  const drawChart = (data) => {
    chartContainer.innerHTML = '';

    const width = 600;
    const height = 400;
    const margin = { top: 20, right: 30, bottom: 30, left: 60 };

    const svg = d3.select(chartContainer)
      .append("svg")
      .attr("width", width + margin.left + margin.right)
      .attr("height", height + margin.top + margin.bottom);

    const g = svg.append("g")
      .attr("transform", `translate(${margin.left},${margin.top})`);

    const colors = ["#1f77b4", "#ff7f0e", "#2ca02c", "#d62728", "#9467bd"];

    const x = d3.scaleLinear()
      .domain([0, d3.max(data)])
      .range([0, width]);

    const y = d3.scaleBand()
      .domain(data.map((_, i) => i))
      .range([0, height])
      .padding(0.1);

    g.selectAll("rect")
      .data(data)
      .enter()
      .append("rect")
      .attr("y", (_, i) => y(i))
      .attr("x", 0)
      .attr("height", y.bandwidth())
      .attr("width", d => x(d))
      .attr("fill", (_, i) => colors[i % colors.length]);

    g.selectAll("text")
      .data(data)
      .enter()
      .append("text")
      .attr("x", d => x(d) + 5)
      .attr("y", (_, i) => y(i) + y.bandwidth() / 2 + 4)
      .text(d => d)
      .style("font-size", "12px")
      .style("fill", "#333");
  };

  updateDataButton.addEventListener("click", () => {
    const rawData = sourceDataInput.value;
    const data = rawData
      .split(',')
      .map(d => parseInt(d.trim()))
      .filter(d => !isNaN(d));

    if (data.length > 0) {
      drawChart(data);
    } else {
      alert("Por favor ingrese números válidos separados por comas");
    }
  });

  updateDataButton.click();
});
