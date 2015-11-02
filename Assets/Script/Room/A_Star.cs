#region File Description
//********************************************************************
//    file name		: A_Star.cs
//    infomation	: A*アルゴリズムのルート選択
//********************************************************************
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

/// <summary>
/// ルート選択
/// </summary>
public static class A_Star
{
    private const int UpHillWeight = 1;

    /// <summary>
    /// ルート選択
    /// </summary>
    /// <param name="_Grids">高低をあらわしたGridの２次配列、差が2以上あるものは壁とみなす</param>
    /// <returns>ルートがあるか</returns>
    public static bool SearchPath(
        int[,] _Grids, 
        Vector2I _startPos, 
        Vector2I _goalPos,
        out List<Vector2I> _PathList)
    {
        GridInfo[,] GridInfos;
        List<GridInfo> _ActiveGrids = new List<GridInfo>();
        int GridWidth = _Grids.GetLength(0);
        int GridHeight = _Grids.GetLength(1);

        // 全グリッド情報を初期化
        GridInfos = new GridInfo[GridWidth, GridHeight];
        for (int y = 0; y < GridHeight; ++y)
        {
            for (int x = 0; x < GridWidth; ++x)
            {
                GridInfos[x, y].pos = new Vector2I(x, y);
                GridInfos[x, y].step = 0;
                GridInfos[x, y].dist = 0;
                GridInfos[x, y].weight = 1e10F;
                GridInfos[x, y].prev = new Vector2I(0, 0);
            }
        }

        // スタート地点をセット
        GridInfo info = GridInfos[_startPos.X, _startPos.Y];
        info.dist = calcDist(_startPos, _goalPos);

        _ActiveGrids.Clear();
        _ActiveGrids.Add(info);

        while (info.pos != _goalPos)
        {
            //周囲4グリッドを計算
            for (int i = 0; i < 4; ++i)
            {
                int sx = ((i % 2 == 0) ? i - 1 : 0);
                int sy = ((i % 2 == 1) ? i - 2 : 0);

                int tx = info.pos.X + sx;
                int ty = info.pos.Y + sy;

                if (tx < 0 || tx > GridWidth - 1 || ty < 0 || ty > GridHeight - 1) continue;

                GridInfo neighbor = GridInfos[tx, ty];

                // 移動コストを計算。平面のA*であれば通常は 1 で問題なし
                float weight = calcStepWeight(_Grids, info.pos, neighbor.pos);
                if (weight < 0) continue; // 0以下を壁とみなし探索を飛ばす
                neighbor.step = info.step + weight;

                neighbor.dist = calcDist(neighbor.pos, _goalPos);
                neighbor.weight = neighbor.step + neighbor.dist;

                neighbor.prev = info.pos;

                // 対象のグリッドがすでに計算されていて、ウェイトが低ければ上書きしない
                if (GridInfos[tx, ty].weight > neighbor.weight)
                {
                    GridInfos[tx, ty] = neighbor;

                    // ウェイトを元に探索対象リストへ挿入
                    bool added = false;
                    for (int j = 0; j < _ActiveGrids.Count; ++j)
                    {
                        if (_ActiveGrids[j].weight > neighbor.weight)
                        {
                            _ActiveGrids.Insert(j, neighbor);
                            added = true;
                            break;
                        }
                    }
                    if (added == false) _ActiveGrids.Add(neighbor);
                }

            }

            //検索済みのグリッドを削除
            _ActiveGrids.Remove(info);

            if (_ActiveGrids.Count() == 0)
            {
                _PathList = null;
                // ルートなし
                return false;
            }

            // 次のグリッドをセット
            info = _ActiveGrids.First();

            // 対象がゴール地点なら検索終了
            if (_goalPos == info.pos)
            {
                _PathList = new List<Vector2I>();
                while (info.pos != _startPos)
                {
                    _PathList.Add(info.pos);
                    info = GridInfos[info.prev.X, info.prev.Y];
                }
                _PathList.Reverse();

                return true;
            }

        }
        _PathList = null;
        return false;
    }

    static float calcStepWeight(int[,] _Grids, Vector2I p1, Vector2I p2)
    {
        // 高さが同一ならば移動コストは通常通り
        if (_Grids[p1.X, p1.Y] == _Grids[p2.X, p2.Y]) return 1;

        // 上り
        if (_Grids[p1.X, p1.Y] < _Grids[p2.X, p2.Y])
        {
            int sh = _Grids[p2.X, p2.Y] - _Grids[p1.X, p1.Y];
            if (sh > 2) return -1; // 今回は2段以上を壁とする
            return 1 + sh * UpHillWeight; // 高さ分コストを加える
        }
        // 下り
        if (_Grids[p1.X, p1.Y] > _Grids[p2.X, p2.Y])
        {
            int sh = _Grids[p1.X, p1.Y] - _Grids[p2.X, p2.Y];
            if (sh > 2) return -1; // 今回は2段以上を壁とする
            return 1; // 下りは同一コストとする
        }
        return -1;
    }

    static float calcDist(Vector2I p1, Vector2I p2)
    {
        float sx = p2.X - p1.X;
        float sy = p2.Y - p1.Y;
        return (float)Math.Sqrt(sx * sx + sy * sy);
    }

    struct GridInfo
    {
        public Vector2I pos;
        public float step;
        public float dist;
        public float weight;
        public Vector2I prev;

        public override string ToString()
        {
            return pos.ToString();
        }
    };
}
