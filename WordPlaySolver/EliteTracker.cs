namespace WordPlaySolver;

/// <summary>
    /// Helper class that will keep track of either the highest or lowest value solutions to a problem
    /// </summary>
    /// <typeparam name="T">Type used to represent a solution</typeparam>
    public class EliteTracker<T> where T : ICloneable
    {
        /// <summary>
        /// The best N solutions ever created and their solution's values/fitness
        /// </summary>
        public List<(T solution, double objectiveValue)> EliteBest { get; private set; }

        /// <summary>
        /// How many solutions to keep track of
        /// </summary>
        public int NumberOfElites { get; }

        /// <summary>
        /// Controls if we are keeping the highest (true) or lowest (false) valued solutions
        /// </summary>
        public bool IsMaximizing { get; }

        public EliteTracker(int numberOfElites, bool isMaximizing)
        {
            NumberOfElites = numberOfElites;
            EliteBest = new List<(T, double)>();
            IsMaximizing = isMaximizing;
        }

        /// <summary>
        /// Gets a specific index of an elite.
        /// NOTE: this will not be the i-th best solution, as we don't actually sort internally
        /// </summary>
        public T GetElite(int index)
        {
            return EliteBest[index].solution;
        }

        /// <summary>
        /// Gets all elites ordered on objective value, with the 'best' always coming first
        /// (smallest for minimization problem, largest for maximization problem)
        /// </summary>
        public IEnumerable<(T, double)> GetElitesInOrder()
        {
            if (IsMaximizing) return EliteBest.OrderByDescending(x => x.objectiveValue);
            return EliteBest.OrderBy(x => x.objectiveValue);
        }

        /// <summary>
        /// Tests if the solution qualifies to be elite based on:
        /// In a maximization problem its solution value  is greater than the lowest value of the elite.
        /// In a minimization problem its solution quality is less than the highest value of the elite.
        /// If successful, the worst elite is replaced by the new solution
        /// </summary>
        /// <param name="solution"></param>
        /// <param name="quality"></param>
        public bool TryUpdateElite(T solution, double quality, bool allowDuplicates)
        {
            if (!allowDuplicates)
            {
                foreach (var best in EliteBest)
                {
                    if (best.solution.ToString() == solution.ToString()) return false;
                }
            }

            if (EliteBest.Count < NumberOfElites)
            {
                EliteBest.Add(((T)solution.Clone(), quality));
                return true;
            }

            if (IsMaximizing)
            {
                var worstEliteIdx = EliteBest.IndexOf(EliteBest.MinBy(eb => eb.objectiveValue));
                if (quality > EliteBest[worstEliteIdx].objectiveValue)
                {
                    EliteBest[worstEliteIdx] = ((T)solution.Clone(), objectiveValue: quality);
                    return true;
                }
            }
            else
            {
                var worstEliteIdx = EliteBest.IndexOf(EliteBest.MaxBy(eb => eb.objectiveValue));
                if (quality < EliteBest[worstEliteIdx].objectiveValue)
                {
                    EliteBest[worstEliteIdx] = ((T)solution.Clone(), objectiveValue: quality);
                    return true;
                }
            }

            return false;
        }
    }