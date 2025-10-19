let charts = {};

function initializeCharts(metricsData) {
    if (metricsData.length <= 1) {
        return;
    }
    // chart configuration common for all charts
    const commonOptions = {
        responsive: true,
        maintainAspectRatio: true,
        plugins: {
            legend: {
                display: false
            }
        },
        scales: {
            y: {
                beginAtZero: false,
                ticks: {
                    font: {
                        size: 10
                    }
                }
            },
            x: {
                ticks: {
                    font: {
                        size: 9
                    }
                }
            }
        }
    };

    // Formats dates data into shorter labels (Month, Day) in stead of having every detail (specific day, month, day number, time, timezone, etc)
    const labels = metricsData.map(m => {
        const date = new Date(m.date);
        return date.toLocaleDateString('en-US', { month: 'short', day: 'numeric' });
    });

    // Weight Chart
    createChart('bodyWeightChart', {
        labels: labels,
        datasets: [{
            label: 'Weight (kg)',
            data: metricsData.map(m => m.weight),
            borderColor: 'rgb(75, 192, 192)',
            backgroundColor: 'rgba(75, 192, 192, 0.1)',
            tension: 0.3,
            fill: true
        }]
    }, commonOptions);

    // Body Fat Chart
    if (metricsData.some(m => m.bodyFat !== null)) {
        createChart('bodyFatChart', {
            labels: labels,
            datasets: [{
                label: 'Body Fat (%)',
                data: metricsData.map(m => m.bodyFat),
                borderColor: 'rgb(255, 99, 132)',
                backgroundColor: 'rgba(255, 99, 132, 0.1)',
                tension: 0.3,
                fill: true,
                spanGaps: true
            }]
        }, commonOptions);
    }

    // Muscle Mass Chart
    if (metricsData.some(m => m.muscleMass !== null)) {
        createChart('muscleMassChart', {
            labels: labels,
            datasets: [{
                label: 'Muscle Mass (%)',
                data: metricsData.map(m => m.muscleMass),
                borderColor: 'rgb(54, 162, 235)',
                backgroundColor: 'rgba(54, 162, 235, 0.1)',
                tension: 0.3,
                fill: true,
                spanGaps: true
            }]
        }, commonOptions);
    }

    // Chest Chart
    if (metricsData.some(m => m.chest !== null)) {
        createChart('chestChart', {
            labels: labels,
            datasets: [{
                label: 'Chest (cm)',
                data: metricsData.map(m => m.chest),
                borderColor: 'rgb(153, 102, 255)',
                backgroundColor: 'rgba(153, 102, 255, 0.1)',
                tension: 0.3,
                fill: true,
                spanGaps: true
            }]
        }, commonOptions);
    }

    // Arms Chart
    if (metricsData.some(m => m.arms !== null)) {
        createChart('armsChart', {
            labels: labels,
            datasets: [{
                label: 'Arms (cm)',
                data: metricsData.map(m => m.arms),
                borderColor: 'rgb(255, 159, 64)',
                backgroundColor: 'rgba(255, 159, 64, 0.1)',
                tension: 0.3,
                fill: true,
                spanGaps: true
            }]
        }, commonOptions);
    }

    // Waist Chart
    if (metricsData.some(m => m.waist !== null)) {
        createChart('waistChart', {
            labels: labels,
            datasets: [{
                label: 'Waist (cm)',
                data: metricsData.map(m => m.waist),
                borderColor: 'rgb(255, 205, 86)',
                backgroundColor: 'rgba(255, 205, 86, 0.1)',
                tension: 0.3,
                fill: true,
                spanGaps: true
            }]
        }, commonOptions);
    }

    // Legs Chart
    if (metricsData.some(m => m.legs !== null)) {
        createChart('legsChart', {
            labels: labels,
            datasets: [{
                label: 'Legs (cm)',
                data: metricsData.map(m => m.legs),
                borderColor: 'rgb(201, 203, 207)',
                backgroundColor: 'rgba(201, 203, 207, 0.1)',
                tension: 0.3,
                fill: true,
                spanGaps: true
            }]
        }, commonOptions);
    }
}

function createChart(canvasId, data, options) {
    const canvas = document.getElementById(canvasId);
    if (!canvas) return;

    if (charts[canvasId]) {
        charts[canvasId].destroy();
    }

    // Create new chart
    const ctx = canvas.getContext('2d');
    charts[canvasId] = new Chart(ctx, {
        type: 'line',
        data: data,
        options: options
    });
}

function initializeDailyCaloriesChart(dailyCaloriesData) {
    if (dailyCaloriesData.length === 0) {
        return;
    }
    const labels = dailyCaloriesData.map(d => d.date);
    const caloriesData = dailyCaloriesData.map(d => d.calories);

    const canvas = document.getElementById('dailyCaloriesChart');
    if (!canvas) return;

    if (charts['dailyCaloriesChart']) {
        charts['dailyCaloriesChart'].destroy();
    }

    const ctx = canvas.getContext('2d');
    charts['dailyCaloriesChart'] = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Calories',
                data: caloriesData,
                backgroundColor: 'rgba(18, 207, 198, 0.5)',
                borderColor: 'rgba(0, 0, 0, 1)',
                borderWidth: 1,
                hoverBackgroundColor: 'rgba(18, 207, 198, 0.8)',
                hoverBorderWidth: 2
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: true,
            plugins: {
                legend: {
                    display: false
                }
            },
            scales: {
                y: {
                    beginAtZero: true,
                    title: {
                        display: true,
                        text: 'Calories'
                    }
                },
                x: {
                    title: {
                        display: true,
                        text: 'Date'
                    }
                }
            }
        }
    });
}

// load charts when the page loads
document.addEventListener('DOMContentLoaded', function() {
    // get  data from the page (will be injected by Razor)
    const metricsDataElement = document.getElementById('metricsData');
    if (metricsDataElement) {
        const metricsData = JSON.parse(metricsDataElement.textContent);
        initializeCharts(metricsData);
    }
    const dailyCaloriesDataElement = document.getElementById('dailyCaloriesData');
    if (dailyCaloriesDataElement) {
        const dailyCaloriesData = JSON.parse(dailyCaloriesDataElement.textContent);
        initializeDailyCaloriesChart(dailyCaloriesData);
    }
});
