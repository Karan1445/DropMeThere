  import React, { useEffect, useState } from "react";
  import axios from "axios";
  import { toast, ToastContainer } from "react-toastify";
  import "react-toastify/dist/ReactToastify.css";
  import "./Dashboard.css";

  const AdminDashboard = () => {
    const [firstColumn, setFirstColumn] = useState("");
    const [dashboardData, setDashboardData] = useState([]);
    const [tableData, setTableData] = useState([]);
    const [columns, setColumns] = useState([]);
    const [activeTab, setActiveTab] = useState("dashboard");
    const [loading, setLoading] = useState(false);
    const [editData, setEditData] = useState(null);
    const [isEditing, setIsEditing] = useState(false);
    const [searchQuery, setSearchQuery] = useState("");
    const [query, setQuery] = useState("");
    const [queryResult, setQueryResult] = useState([]);

    useEffect(() => {
      if(localStorage.getItem("adminID")!=1){
        window.location.href="/";
      }
      setLoading(true);
      axios.get("http://localhost:5036/dashboardData")
        .then(response => setDashboardData(response.data))
        .catch(error => console.error("Error fetching dashboard data:", error))
        .finally(() => setLoading(false));
    }, []);

    const fetchTableData = (tableName) => {
      setLoading(true);
      axios.get(`http://localhost:5036/api/AdminData?Data=${encodeURIComponent(`SELECT * FROM ${tableName}`)}`)
        .then(response => {
          if (response.data.length > 0) {
            const keys = Object.keys(response.data[0]);
            setColumns(keys);
            setFirstColumn(keys[0]);
            setTableData(response.data);
          } else {
            setColumns([]);
            setTableData([]);
          }
        })
        .catch(error => console.error(`Error fetching data for ${tableName}:`, error))
        .finally(() => setLoading(false));
    };

    const handleDelete = (uniqueValue) => {
      if (window.confirm("Are you sure you want to delete this record?")) {
        const query = `DELETE FROM ${activeTab} WHERE ${firstColumn}='${uniqueValue}'`;
        axios.get("http://localhost:5036/api/AdminData?Data=" + query)
          .then(() => {
            setTableData(prevData => prevData.filter(item => item[firstColumn] !== uniqueValue));
            toast.success("Record deleted successfully!");
          })
          .catch(() => toast.error("Failed to delete record!"));
      }
    };

    const handleEdit = (row) => {
      setEditData(row);
      setIsEditing(true);
    
    };

    const handleUpdate = () => {
      const updateQuery = `UPDATE ${activeTab} SET ${Object.keys(editData)
        .filter(col => col !== firstColumn)
        .map(col => `${col}='${editData[col]}'`)
        .join(", ")} WHERE ${firstColumn}=${editData[firstColumn]}`;

      axios.get("http://localhost:5036/api/AdminData?Data=" + updateQuery)
        .then(() => {
          setTableData(prevData => prevData.map(item => (item[firstColumn] === editData[firstColumn] ? editData : item)));
          toast.success("Record updated successfully!");
          setIsEditing(false);
          setEditData(null);
        })
        .catch(() => toast.error("Failed to update record!"));
    };

    const filteredTableData = tableData.filter(row =>
      columns.some(col => row[col]?.toString().toLowerCase().includes(searchQuery.toLowerCase()))
    );

    const downloadCSV = () => {
      const csvData = [columns.join(","), ...tableData.map(row => columns.map(col => row[col]).join(","))].join("\n");
      const blob = new Blob([csvData], { type: "text/csv" });
      const url = URL.createObjectURL(blob);
      const a = document.createElement("a");
      a.href = url;
      a.download = `${activeTab}.csv`;
      a.click();
      URL.revokeObjectURL(url);
    };
    const executeQuery = () => {
      if (!query.trim()) {
        toast.error("Please enter a query!");
        return;
      }
      setLoading(true);
      axios.get(`http://localhost:5036/api/AdminData?Data=${encodeURIComponent(query)}`)
        .then(response => setQueryResult(response.data))
        .catch(error => toast.error("Error executing query!"))
        .finally(() => setLoading(false));
    };
    return (
      <>
        <div className="dashboard-container">
          <div className="sidebar">
            <h2 className="sidebar-title">Admin Panel</h2>
            <button className={`sidebar-button ${activeTab === "dashboard" ? "active" : ""}`} onClick={() => setActiveTab("dashboard")}>Dashboard</button>
            <div className="sidebar-list">
              {dashboardData.map((item) => (
                <button key={item.tableName} className={`sidebar-button ${activeTab === item.tableName ? "active" : ""}`} onClick={() => {
                  setActiveTab(item.tableName);
                  fetchTableData(item.tableName);
                }}>
                  {item.tableName} ({item.totalRows})
                </button>
              ))}
            </div>
          </div>

          <div className="main-content">
            {activeTab === "dashboard" ? (
              <div className="dashboard">
                <h1 className="dashboard-title">Admin Dashboard</h1>
                {loading ? <p className="loading-text">Loading...</p> : (
                  <div className="dashboard-grid">
                    {dashboardData.map((item) => (
                      <div key={item.tableName} className="dashboard-card">
                        <h3>{item.tableName}</h3>
                        <p>{item.totalRows}</p>
                      </div>
                    ))}
                  </div>
                )}             
                 <div className="query-section">
                <input
                  type="text"
                  placeholder="Enter SQL Query..."
                  value={query}
                  onChange={(e) => setQuery(e.target.value)}
                  className="query-box"
                />
                <button className="execute-button" onClick={executeQuery}>Execute</button>
              </div>
              <div className="query-result">
                {loading ? <p>Loading...</p> : queryResult.length > 0 ? (
                  <table className="query-table">
                    <thead>
                      <tr>
                        {Object.keys(queryResult[0]).map((col, index) => (
                          <th key={index}>{col}</th>
                        ))}
                      </tr>
                    </thead>
                    <tbody>
                      {queryResult.map((row, index) => (
                        <tr key={index}>
                          {Object.values(row).map((val, i) => (
                            <td key={i}>{JSON.stringify(val)}</td>
                          ))}
                        </tr>
                      ))}
                    </tbody>
                  </table>
                ) : <p>No results found.</p>}
              </div>
         
              </div>
            ) : (
              <div className="table-container">
                <div className="table-header">
                  {activeTab} Data
                  <div class="search-download-container">
                  <input type="text"  placeholder="Search..." value={searchQuery} onChange={(e) => setSearchQuery(e.target.value)} className="search-box" />
                  <button className="download-button" onClick={downloadCSV}>Download CSV</button>
                  </div>
                </div>
                <div className="table-wrapper">
                  {loading ? <p className="loading-text">Loading...</p> : columns.length > 0 ? (
                    <table className="data-table">
                      <thead>
                        <tr>
                          <th>Actions</th>
                          {columns.map((col, index) => (
                            <th key={index}>{col}</th>
                          ))}
                        </tr>
                      </thead>
                      <tbody>
                        {filteredTableData.map((row, index) => (
                          <tr key={index}>
                            <td>
                              <button className="edit-button" onClick={() => handleEdit(row)}>Edit</button>
                              <button className="delete-button" onClick={() => handleDelete(row[firstColumn])}>Delete</button>
                            </td>
                            {columns.map((col, colIndex) => (
                              <td key={colIndex}>{JSON.stringify(row[col])}</td>
                            ))}
                          </tr>
                        ))}
                      </tbody>
                    </table>
                  ) : <p className="loading-text">No records found.</p>}
                </div>
              </div>
            )}
          </div>
        </div>
          {/* Edit Modal */}
          {isEditing && (
          <div className="modal-overlay">
            <div className="modal-content">
              <h2 >Edit Record</h2>
              {columns.map((col, index) => (
                <div key={index} className="input-group">
                  <label>{col}</label>
                  <input type="text" value={editData[col]} onChange={(e) => setEditData({ ...editData, [col]: e.target.value })} />
                </div>
              ))}
              <div className="modal-buttons">
                <button className="save-button" onClick={handleUpdate}>Save</button>
                <button className="cancel-button" onClick={() => setIsEditing(false)}>Cancel</button>
              </div>
            </div>
          </div>
        )}
        <ToastContainer />
      </>
    );
  };

  export default AdminDashboard;